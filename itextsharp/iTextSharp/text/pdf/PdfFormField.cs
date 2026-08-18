using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200058D RID: 1421
	public class PdfFormField : PdfAnnotation
	{
		// Token: 0x06003061 RID: 12385 RVA: 0x0012B6BA File Offset: 0x0012A6BA
		public PdfFormField(PdfWriter writer, float llx, float lly, float urx, float ury, PdfAction action) : base(writer, llx, lly, urx, ury, action)
		{
			base.Put(PdfName.TYPE, PdfName.ANNOT);
			base.Put(PdfName.SUBTYPE, PdfName.WIDGET);
			this.annotation = true;
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x0012B6F2 File Offset: 0x0012A6F2
		internal PdfFormField(PdfWriter writer) : base(writer, null)
		{
			this.form = true;
			this.annotation = false;
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x0012B70C File Offset: 0x0012A70C
		public void SetWidget(Rectangle rect, PdfName highlight)
		{
			base.Put(PdfName.TYPE, PdfName.ANNOT);
			base.Put(PdfName.SUBTYPE, PdfName.WIDGET);
			base.Put(PdfName.RECT, new PdfRectangle(rect));
			this.annotation = true;
			if (highlight != null && !highlight.Equals(PdfAnnotation.HIGHLIGHT_INVERT))
			{
				base.Put(PdfName.H, highlight);
			}
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x0012B770 File Offset: 0x0012A770
		public static PdfFormField CreateEmpty(PdfWriter writer)
		{
			return new PdfFormField(writer);
		}

		// Token: 0x1700084A RID: 2122
		// (set) Token: 0x06003065 RID: 12389 RVA: 0x0012B785 File Offset: 0x0012A785
		public int Button
		{
			set
			{
				base.Put(PdfName.FT, PdfName.BTN);
				if (value != 0)
				{
					base.Put(PdfName.FF, new PdfNumber(value));
				}
			}
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x0012B7AC File Offset: 0x0012A7AC
		protected static PdfFormField CreateButton(PdfWriter writer, int flags)
		{
			return new PdfFormField(writer)
			{
				Button = flags
			};
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x0012B7C8 File Offset: 0x0012A7C8
		public static PdfFormField CreatePushButton(PdfWriter writer)
		{
			return PdfFormField.CreateButton(writer, 65536);
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x0012B7D5 File Offset: 0x0012A7D5
		public static PdfFormField CreateCheckBox(PdfWriter writer)
		{
			return PdfFormField.CreateButton(writer, 0);
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x0012B7DE File Offset: 0x0012A7DE
		public static PdfFormField CreateRadioButton(PdfWriter writer, bool noToggleToOff)
		{
			return PdfFormField.CreateButton(writer, 32768 + (noToggleToOff ? 16384 : 0));
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x0012B7F8 File Offset: 0x0012A7F8
		public static PdfFormField CreateTextField(PdfWriter writer, bool multiline, bool password, int maxLen)
		{
			PdfFormField pdfFormField = new PdfFormField(writer);
			pdfFormField.Put(PdfName.FT, PdfName.TX);
			int num = multiline ? 4096 : 0;
			num += (password ? 8192 : 0);
			pdfFormField.Put(PdfName.FF, new PdfNumber(num));
			if (maxLen > 0)
			{
				pdfFormField.Put(PdfName.MAXLEN, new PdfNumber(maxLen));
			}
			return pdfFormField;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x0012B860 File Offset: 0x0012A860
		protected static PdfFormField CreateChoice(PdfWriter writer, int flags, PdfArray options, int topIndex)
		{
			PdfFormField pdfFormField = new PdfFormField(writer);
			pdfFormField.Put(PdfName.FT, PdfName.CH);
			pdfFormField.Put(PdfName.FF, new PdfNumber(flags));
			pdfFormField.Put(PdfName.OPT, options);
			if (topIndex > 0)
			{
				pdfFormField.Put(PdfName.TI, new PdfNumber(topIndex));
			}
			return pdfFormField;
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x0012B8B7 File Offset: 0x0012A8B7
		public static PdfFormField CreateList(PdfWriter writer, string[] options, int topIndex)
		{
			return PdfFormField.CreateChoice(writer, 0, PdfFormField.ProcessOptions(options), topIndex);
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x0012B8C7 File Offset: 0x0012A8C7
		public static PdfFormField CreateList(PdfWriter writer, string[,] options, int topIndex)
		{
			return PdfFormField.CreateChoice(writer, 0, PdfFormField.ProcessOptions(options), topIndex);
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x0012B8D7 File Offset: 0x0012A8D7
		public static PdfFormField CreateCombo(PdfWriter writer, bool edit, string[] options, int topIndex)
		{
			return PdfFormField.CreateChoice(writer, 131072 + (edit ? 262144 : 0), PdfFormField.ProcessOptions(options), topIndex);
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x0012B8F7 File Offset: 0x0012A8F7
		public static PdfFormField CreateCombo(PdfWriter writer, bool edit, string[,] options, int topIndex)
		{
			return PdfFormField.CreateChoice(writer, 131072 + (edit ? 262144 : 0), PdfFormField.ProcessOptions(options), topIndex);
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x0012B918 File Offset: 0x0012A918
		protected static PdfArray ProcessOptions(string[] options)
		{
			PdfArray pdfArray = new PdfArray();
			for (int i = 0; i < options.Length; i++)
			{
				pdfArray.Add(new PdfString(options[i], "UnicodeBig"));
			}
			return pdfArray;
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x0012B950 File Offset: 0x0012A950
		protected static PdfArray ProcessOptions(string[,] options)
		{
			PdfArray pdfArray = new PdfArray();
			for (int i = 0; i < options.GetLength(0); i++)
			{
				PdfArray pdfArray2 = new PdfArray(new PdfString(options[i, 0], "UnicodeBig"));
				pdfArray2.Add(new PdfString(options[i, 1], "UnicodeBig"));
				pdfArray.Add(pdfArray2);
			}
			return pdfArray;
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x0012B9B0 File Offset: 0x0012A9B0
		public static PdfFormField CreateSignature(PdfWriter writer)
		{
			PdfFormField pdfFormField = new PdfFormField(writer);
			pdfFormField.Put(PdfName.FT, PdfName.SIG);
			return pdfFormField;
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06003073 RID: 12403 RVA: 0x0012B9D5 File Offset: 0x0012A9D5
		public PdfFormField Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x0012B9DD File Offset: 0x0012A9DD
		public void AddKid(PdfFormField field)
		{
			field.parent = this;
			if (this.kids == null)
			{
				this.kids = new List<PdfFormField>();
			}
			this.kids.Add(field);
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06003075 RID: 12405 RVA: 0x0012BA05 File Offset: 0x0012AA05
		public List<PdfFormField> Kids
		{
			get
			{
				return this.kids;
			}
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x0012BA10 File Offset: 0x0012AA10
		public int SetFieldFlags(int flags)
		{
			PdfNumber pdfNumber = (PdfNumber)base.Get(PdfName.FF);
			int num;
			if (pdfNumber == null)
			{
				num = 0;
			}
			else
			{
				num = pdfNumber.IntValue;
			}
			int value = num | flags;
			base.Put(PdfName.FF, new PdfNumber(value));
			return num;
		}

		// Token: 0x1700084D RID: 2125
		// (set) Token: 0x06003077 RID: 12407 RVA: 0x0012BA52 File Offset: 0x0012AA52
		public string ValueAsString
		{
			set
			{
				base.Put(PdfName.V, new PdfString(value, "UnicodeBig"));
			}
		}

		// Token: 0x1700084E RID: 2126
		// (set) Token: 0x06003078 RID: 12408 RVA: 0x0012BA6A File Offset: 0x0012AA6A
		public string ValueAsName
		{
			set
			{
				base.Put(PdfName.V, new PdfName(value));
			}
		}

		// Token: 0x1700084F RID: 2127
		// (set) Token: 0x06003079 RID: 12409 RVA: 0x0012BA7D File Offset: 0x0012AA7D
		public PdfSignature ValueAsSig
		{
			set
			{
				base.Put(PdfName.V, value);
			}
		}

		// Token: 0x17000850 RID: 2128
		// (set) Token: 0x0600307A RID: 12410 RVA: 0x0012BA8B File Offset: 0x0012AA8B
		public string DefaultValueAsString
		{
			set
			{
				base.Put(PdfName.DV, new PdfString(value, "UnicodeBig"));
			}
		}

		// Token: 0x17000851 RID: 2129
		// (set) Token: 0x0600307B RID: 12411 RVA: 0x0012BAA3 File Offset: 0x0012AAA3
		public string DefaultValueAsName
		{
			set
			{
				base.Put(PdfName.DV, new PdfName(value));
			}
		}

		// Token: 0x17000852 RID: 2130
		// (set) Token: 0x0600307C RID: 12412 RVA: 0x0012BAB6 File Offset: 0x0012AAB6
		public string FieldName
		{
			set
			{
				if (value != null)
				{
					base.Put(PdfName.T, new PdfString(value, "UnicodeBig"));
				}
			}
		}

		// Token: 0x17000853 RID: 2131
		// (set) Token: 0x0600307D RID: 12413 RVA: 0x0012BAD1 File Offset: 0x0012AAD1
		public string UserName
		{
			set
			{
				base.Put(PdfName.TU, new PdfString(value, "UnicodeBig"));
			}
		}

		// Token: 0x17000854 RID: 2132
		// (set) Token: 0x0600307E RID: 12414 RVA: 0x0012BAE9 File Offset: 0x0012AAE9
		public string MappingName
		{
			set
			{
				base.Put(PdfName.TM, new PdfString(value, "UnicodeBig"));
			}
		}

		// Token: 0x17000855 RID: 2133
		// (set) Token: 0x0600307F RID: 12415 RVA: 0x0012BB01 File Offset: 0x0012AB01
		public int Quadding
		{
			set
			{
				base.Put(PdfName.Q, new PdfNumber(value));
			}
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x0012BB14 File Offset: 0x0012AB14
		internal static void MergeResources(PdfDictionary result, PdfDictionary source, PdfStamperImp writer)
		{
			for (int i = 0; i < PdfFormField.mergeTarget.Length; i++)
			{
				PdfName key = PdfFormField.mergeTarget[i];
				PdfDictionary asDict = source.GetAsDict(key);
				PdfDictionary other;
				if ((other = asDict) != null)
				{
					PdfDictionary pdfDictionary = ((PdfDictionary)PdfReader.GetPdfObject(result.Get(key), result)) ?? new PdfDictionary();
					pdfDictionary.MergeDifferent(other);
					result.Put(key, pdfDictionary);
					if (writer != null)
					{
						writer.MarkUsed(pdfDictionary);
					}
				}
			}
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x0012BB85 File Offset: 0x0012AB85
		internal static void MergeResources(PdfDictionary result, PdfDictionary source)
		{
			PdfFormField.MergeResources(result, source, null);
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x0012BB90 File Offset: 0x0012AB90
		public override void SetUsed()
		{
			this.used = true;
			if (this.parent != null)
			{
				base.Put(PdfName.PARENT, this.parent.IndirectReference);
			}
			if (this.kids != null)
			{
				PdfArray pdfArray = new PdfArray();
				for (int i = 0; i < this.kids.Count; i++)
				{
					pdfArray.Add(this.kids[i].IndirectReference);
				}
				base.Put(PdfName.KIDS, pdfArray);
			}
			if (this.templates == null)
			{
				return;
			}
			PdfDictionary pdfDictionary = new PdfDictionary();
			foreach (PdfTemplate pdfTemplate in this.templates.Keys)
			{
				PdfFormField.MergeResources(pdfDictionary, (PdfDictionary)pdfTemplate.Resources);
			}
			base.Put(PdfName.DR, pdfDictionary);
		}

		// Token: 0x0400213A RID: 8506
		public const int FF_READ_ONLY = 1;

		// Token: 0x0400213B RID: 8507
		public const int FF_REQUIRED = 2;

		// Token: 0x0400213C RID: 8508
		public const int FF_NO_EXPORT = 4;

		// Token: 0x0400213D RID: 8509
		public const int FF_NO_TOGGLE_TO_OFF = 16384;

		// Token: 0x0400213E RID: 8510
		public const int FF_RADIO = 32768;

		// Token: 0x0400213F RID: 8511
		public const int FF_PUSHBUTTON = 65536;

		// Token: 0x04002140 RID: 8512
		public const int FF_MULTILINE = 4096;

		// Token: 0x04002141 RID: 8513
		public const int FF_PASSWORD = 8192;

		// Token: 0x04002142 RID: 8514
		public const int FF_COMBO = 131072;

		// Token: 0x04002143 RID: 8515
		public const int FF_EDIT = 262144;

		// Token: 0x04002144 RID: 8516
		public const int FF_FILESELECT = 1048576;

		// Token: 0x04002145 RID: 8517
		public const int FF_MULTISELECT = 2097152;

		// Token: 0x04002146 RID: 8518
		public const int FF_DONOTSPELLCHECK = 4194304;

		// Token: 0x04002147 RID: 8519
		public const int FF_DONOTSCROLL = 8388608;

		// Token: 0x04002148 RID: 8520
		public const int FF_COMB = 16777216;

		// Token: 0x04002149 RID: 8521
		public const int FF_RADIOSINUNISON = 33554432;

		// Token: 0x0400214A RID: 8522
		public const int Q_LEFT = 0;

		// Token: 0x0400214B RID: 8523
		public const int Q_CENTER = 1;

		// Token: 0x0400214C RID: 8524
		public const int Q_RIGHT = 2;

		// Token: 0x0400214D RID: 8525
		public const int MK_NO_ICON = 0;

		// Token: 0x0400214E RID: 8526
		public const int MK_NO_CAPTION = 1;

		// Token: 0x0400214F RID: 8527
		public const int MK_CAPTION_BELOW = 2;

		// Token: 0x04002150 RID: 8528
		public const int MK_CAPTION_ABOVE = 3;

		// Token: 0x04002151 RID: 8529
		public const int MK_CAPTION_RIGHT = 4;

		// Token: 0x04002152 RID: 8530
		public const int MK_CAPTION_LEFT = 5;

		// Token: 0x04002153 RID: 8531
		public const int MK_CAPTION_OVERLAID = 6;

		// Token: 0x04002154 RID: 8532
		public const bool MULTILINE = true;

		// Token: 0x04002155 RID: 8533
		public const bool SINGLELINE = false;

		// Token: 0x04002156 RID: 8534
		public const bool PLAINTEXT = false;

		// Token: 0x04002157 RID: 8535
		public const bool PASSWORD = true;

		// Token: 0x04002158 RID: 8536
		public static readonly PdfName IF_SCALE_ALWAYS = PdfName.A;

		// Token: 0x04002159 RID: 8537
		public static readonly PdfName IF_SCALE_BIGGER = PdfName.B;

		// Token: 0x0400215A RID: 8538
		public static readonly PdfName IF_SCALE_SMALLER = PdfName.S;

		// Token: 0x0400215B RID: 8539
		public static readonly PdfName IF_SCALE_NEVER = PdfName.N;

		// Token: 0x0400215C RID: 8540
		public static readonly PdfName IF_SCALE_ANAMORPHIC = PdfName.A;

		// Token: 0x0400215D RID: 8541
		public static readonly PdfName IF_SCALE_PROPORTIONAL = PdfName.P;

		// Token: 0x0400215E RID: 8542
		public static PdfName[] mergeTarget = new PdfName[]
		{
			PdfName.FONT,
			PdfName.XOBJECT,
			PdfName.COLORSPACE,
			PdfName.PATTERN
		};

		// Token: 0x0400215F RID: 8543
		internal PdfFormField parent;

		// Token: 0x04002160 RID: 8544
		internal List<PdfFormField> kids;
	}
}
