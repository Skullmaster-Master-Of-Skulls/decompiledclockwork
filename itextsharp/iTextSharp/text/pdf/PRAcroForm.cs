using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000271 RID: 625
	public class PRAcroForm : PdfDictionary
	{
		// Token: 0x06001792 RID: 6034 RVA: 0x0008719F File Offset: 0x0008619F
		public PRAcroForm(PdfReader reader)
		{
			this.reader = reader;
			this.fields = new List<PRAcroForm.FieldInformation>();
			this.fieldByName = new Dictionary<string, PRAcroForm.FieldInformation>();
			this.stack = new List<PdfDictionary>();
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x000871CF File Offset: 0x000861CF
		public new int Size
		{
			get
			{
				return this.fields.Count;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x000871DC File Offset: 0x000861DC
		public List<PRAcroForm.FieldInformation> Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x000871E4 File Offset: 0x000861E4
		public PRAcroForm.FieldInformation GetField(string name)
		{
			PRAcroForm.FieldInformation result;
			this.fieldByName.TryGetValue(name, out result);
			return result;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00087204 File Offset: 0x00086204
		public PRIndirectReference GetRefByName(string name)
		{
			PRAcroForm.FieldInformation field = this.GetField(name);
			if (field == null)
			{
				return null;
			}
			return field.Ref;
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00087224 File Offset: 0x00086224
		public void ReadAcroForm(PdfDictionary root)
		{
			if (root == null)
			{
				return;
			}
			this.hashMap = root.hashMap;
			this.PushAttrib(root);
			PdfArray fieldlist = (PdfArray)PdfReader.GetPdfObjectRelease(root.Get(PdfName.FIELDS));
			this.IterateFields(fieldlist, null, null);
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00087268 File Offset: 0x00086268
		protected void IterateFields(PdfArray fieldlist, PRIndirectReference fieldDict, string title)
		{
			foreach (PdfObject pdfObject in fieldlist.ArrayList)
			{
				PRIndirectReference prindirectReference = (PRIndirectReference)pdfObject;
				PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(prindirectReference);
				PRIndirectReference prindirectReference2 = fieldDict;
				string text = title;
				PdfString pdfString = (PdfString)pdfDictionary.Get(PdfName.T);
				bool flag = pdfString != null;
				if (flag)
				{
					prindirectReference2 = prindirectReference;
					if (title == null)
					{
						text = pdfString.ToString();
					}
					else
					{
						text = title + '.' + pdfString.ToString();
					}
				}
				PdfArray pdfArray = (PdfArray)pdfDictionary.Get(PdfName.KIDS);
				if (pdfArray != null)
				{
					this.PushAttrib(pdfDictionary);
					this.IterateFields(pdfArray, prindirectReference2, text);
					this.stack.RemoveAt(this.stack.Count - 1);
				}
				else if (prindirectReference2 != null)
				{
					PdfDictionary pdfDictionary2 = this.stack[this.stack.Count - 1];
					if (flag)
					{
						pdfDictionary2 = this.MergeAttrib(pdfDictionary2, pdfDictionary);
					}
					pdfDictionary2.Put(PdfName.T, new PdfString(text));
					PRAcroForm.FieldInformation fieldInformation = new PRAcroForm.FieldInformation(text, pdfDictionary2, prindirectReference2);
					this.fields.Add(fieldInformation);
					this.fieldByName[text] = fieldInformation;
				}
			}
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x000873C8 File Offset: 0x000863C8
		protected PdfDictionary MergeAttrib(PdfDictionary parent, PdfDictionary child)
		{
			PdfDictionary pdfDictionary = new PdfDictionary();
			if (parent != null)
			{
				pdfDictionary.Merge(parent);
			}
			foreach (PdfName pdfName in child.Keys)
			{
				if (pdfName.Equals(PdfName.DR) || pdfName.Equals(PdfName.DA) || pdfName.Equals(PdfName.Q) || pdfName.Equals(PdfName.FF) || pdfName.Equals(PdfName.DV) || pdfName.Equals(PdfName.V) || pdfName.Equals(PdfName.FT) || pdfName.Equals(PdfName.F))
				{
					pdfDictionary.Put(pdfName, child.Get(pdfName));
				}
			}
			return pdfDictionary;
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x000874A0 File Offset: 0x000864A0
		protected void PushAttrib(PdfDictionary dict)
		{
			PdfDictionary pdfDictionary = null;
			if (this.stack.Count != 0)
			{
				pdfDictionary = this.stack[this.stack.Count - 1];
			}
			pdfDictionary = this.MergeAttrib(pdfDictionary, dict);
			this.stack.Add(pdfDictionary);
		}

		// Token: 0x0400100B RID: 4107
		internal List<PRAcroForm.FieldInformation> fields;

		// Token: 0x0400100C RID: 4108
		internal List<PdfDictionary> stack;

		// Token: 0x0400100D RID: 4109
		internal Dictionary<string, PRAcroForm.FieldInformation> fieldByName;

		// Token: 0x0400100E RID: 4110
		internal PdfReader reader;

		// Token: 0x02000272 RID: 626
		public class FieldInformation
		{
			// Token: 0x0600179B RID: 6043 RVA: 0x000874EA File Offset: 0x000864EA
			internal FieldInformation(string name, PdfDictionary info, PRIndirectReference refi)
			{
				this.name = name;
				this.info = info;
				this.refi = refi;
			}

			// Token: 0x1700044A RID: 1098
			// (get) Token: 0x0600179C RID: 6044 RVA: 0x00087507 File Offset: 0x00086507
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x1700044B RID: 1099
			// (get) Token: 0x0600179D RID: 6045 RVA: 0x0008750F File Offset: 0x0008650F
			public PdfDictionary Info
			{
				get
				{
					return this.info;
				}
			}

			// Token: 0x1700044C RID: 1100
			// (get) Token: 0x0600179E RID: 6046 RVA: 0x00087517 File Offset: 0x00086517
			public PRIndirectReference Ref
			{
				get
				{
					return this.refi;
				}
			}

			// Token: 0x0400100F RID: 4111
			internal string name;

			// Token: 0x04001010 RID: 4112
			internal PdfDictionary info;

			// Token: 0x04001011 RID: 4113
			internal PRIndirectReference refi;
		}
	}
}
