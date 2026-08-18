using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf
{
	// Token: 0x020004E6 RID: 1254
	public class PageResources
	{
		// Token: 0x06002ADD RID: 10973 RVA: 0x00104658 File Offset: 0x00103658
		internal PageResources()
		{
			int[] array = new int[1];
			this.namePtr = array;
			base..ctor();
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x001046C8 File Offset: 0x001036C8
		internal void SetOriginalResources(PdfDictionary resources, int[] newNamePtr)
		{
			if (newNamePtr != null)
			{
				this.namePtr = newNamePtr;
			}
			this.forbiddenNames = new Dictionary<PdfName, object>();
			this.usedNames = new Dictionary<PdfName, PdfName>();
			if (resources == null)
			{
				return;
			}
			this.originalResources = new PdfDictionary();
			this.originalResources.Merge(resources);
			foreach (PdfName key in resources.Keys)
			{
				PdfObject pdfObject = PdfReader.GetPdfObject(resources.Get(key));
				if (pdfObject != null && pdfObject.IsDictionary())
				{
					PdfDictionary pdfDictionary = (PdfDictionary)pdfObject;
					foreach (PdfName key2 in pdfDictionary.Keys)
					{
						this.forbiddenNames[key2] = null;
					}
					PdfDictionary pdfDictionary2 = new PdfDictionary();
					pdfDictionary2.Merge(pdfDictionary);
					this.originalResources.Put(key, pdfDictionary2);
				}
			}
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x001047DC File Offset: 0x001037DC
		internal PdfName TranslateName(PdfName name)
		{
			PdfName pdfName = name;
			if (this.forbiddenNames != null)
			{
				this.usedNames.TryGetValue(name, out pdfName);
				if (pdfName == null)
				{
					do
					{
						pdfName = new PdfName("Xi" + this.namePtr[0]++);
					}
					while (this.forbiddenNames.ContainsKey(pdfName));
					this.usedNames[name] = pdfName;
				}
			}
			return pdfName;
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x00104852 File Offset: 0x00103852
		internal PdfName AddFont(PdfName name, PdfIndirectReference reference)
		{
			name = this.TranslateName(name);
			this.fontDictionary.Put(name, reference);
			return name;
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x0010486B File Offset: 0x0010386B
		internal PdfName AddXObject(PdfName name, PdfIndirectReference reference)
		{
			name = this.TranslateName(name);
			this.xObjectDictionary.Put(name, reference);
			return name;
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x00104884 File Offset: 0x00103884
		internal PdfName AddColor(PdfName name, PdfIndirectReference reference)
		{
			name = this.TranslateName(name);
			this.colorDictionary.Put(name, reference);
			return name;
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x0010489D File Offset: 0x0010389D
		internal void AddDefaultColor(PdfName name, PdfObject obj)
		{
			if (obj == null || obj.IsNull())
			{
				this.colorDictionary.Remove(name);
				return;
			}
			this.colorDictionary.Put(name, obj);
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x001048C4 File Offset: 0x001038C4
		internal void AddDefaultColor(PdfDictionary dic)
		{
			this.colorDictionary.Merge(dic);
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x001048D2 File Offset: 0x001038D2
		internal void AddDefaultColorDiff(PdfDictionary dic)
		{
			this.colorDictionary.MergeDifferent(dic);
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x001048E0 File Offset: 0x001038E0
		internal PdfName AddShading(PdfName name, PdfIndirectReference reference)
		{
			name = this.TranslateName(name);
			this.shadingDictionary.Put(name, reference);
			return name;
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x001048F9 File Offset: 0x001038F9
		internal PdfName AddPattern(PdfName name, PdfIndirectReference reference)
		{
			name = this.TranslateName(name);
			this.patternDictionary.Put(name, reference);
			return name;
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x00104912 File Offset: 0x00103912
		internal PdfName AddExtGState(PdfName name, PdfIndirectReference reference)
		{
			name = this.TranslateName(name);
			this.extGStateDictionary.Put(name, reference);
			return name;
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x0010492B File Offset: 0x0010392B
		internal PdfName AddProperty(PdfName name, PdfIndirectReference reference)
		{
			name = this.TranslateName(name);
			this.propertyDictionary.Put(name, reference);
			return name;
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06002AEA RID: 10986 RVA: 0x00104944 File Offset: 0x00103944
		internal PdfDictionary Resources
		{
			get
			{
				PdfResources pdfResources = new PdfResources();
				if (this.originalResources != null)
				{
					pdfResources.Merge(this.originalResources);
				}
				pdfResources.Put(PdfName.PROCSET, new PdfLiteral("[/PDF /Text /ImageB /ImageC /ImageI]"));
				pdfResources.Add(PdfName.FONT, this.fontDictionary);
				pdfResources.Add(PdfName.XOBJECT, this.xObjectDictionary);
				pdfResources.Add(PdfName.COLORSPACE, this.colorDictionary);
				pdfResources.Add(PdfName.PATTERN, this.patternDictionary);
				pdfResources.Add(PdfName.SHADING, this.shadingDictionary);
				pdfResources.Add(PdfName.EXTGSTATE, this.extGStateDictionary);
				pdfResources.Add(PdfName.PROPERTIES, this.propertyDictionary);
				return pdfResources;
			}
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x001049F8 File Offset: 0x001039F8
		internal bool HasResources()
		{
			return this.fontDictionary.Size > 0 || this.xObjectDictionary.Size > 0 || this.colorDictionary.Size > 0 || this.patternDictionary.Size > 0 || this.shadingDictionary.Size > 0 || this.extGStateDictionary.Size > 0 || this.propertyDictionary.Size > 0;
		}

		// Token: 0x04001DA5 RID: 7589
		protected PdfDictionary fontDictionary = new PdfDictionary();

		// Token: 0x04001DA6 RID: 7590
		protected PdfDictionary xObjectDictionary = new PdfDictionary();

		// Token: 0x04001DA7 RID: 7591
		protected PdfDictionary colorDictionary = new PdfDictionary();

		// Token: 0x04001DA8 RID: 7592
		protected PdfDictionary patternDictionary = new PdfDictionary();

		// Token: 0x04001DA9 RID: 7593
		protected PdfDictionary shadingDictionary = new PdfDictionary();

		// Token: 0x04001DAA RID: 7594
		protected PdfDictionary extGStateDictionary = new PdfDictionary();

		// Token: 0x04001DAB RID: 7595
		protected PdfDictionary propertyDictionary = new PdfDictionary();

		// Token: 0x04001DAC RID: 7596
		protected Dictionary<PdfName, object> forbiddenNames;

		// Token: 0x04001DAD RID: 7597
		protected PdfDictionary originalResources;

		// Token: 0x04001DAE RID: 7598
		protected int[] namePtr;

		// Token: 0x04001DAF RID: 7599
		protected Dictionary<PdfName, PdfName> usedNames;
	}
}
