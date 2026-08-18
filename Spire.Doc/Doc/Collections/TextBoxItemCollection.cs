using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x02000540 RID: 1344
	public class TextBoxItemCollection : DocumentObjectCollection, ITextBoxItemCollection
	{
		// Token: 0x17000556 RID: 1366
		public ITextBox this[int index]
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
				return (ITextBox)base.InnerList[index];
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x0600462E RID: 17966 RVA: 0x0040DD18 File Offset: 0x0040CD18
		protected override Type[] TypesOfElement
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
				return TextBoxItemCollection.ᜀ;
			}
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x0040DD58 File Offset: 0x0040CD58
		public TextBoxItemCollection(IDocument doc) : base((Document)doc, (Document)doc)
		{
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x0040DD78 File Offset: 0x0040CD78
		public int Add(ITextBox textBox)
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
			return base.InnerList.Add(textBox);
		}

		// Token: 0x06004631 RID: 17969 RVA: 0x0040DDC0 File Offset: 0x0040CDC0
		protected override OwnerHolder CreateItem(IXDLSContentReader reader)
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
			return base.Document.CreateParagraphItem(ParagraphItemType.TextBox);
		}

		// Token: 0x06004632 RID: 17970 RVA: 0x0040DE08 File Offset: 0x0040CE08
		protected override string GetTagItemName()
		{
			int a_ = 13;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return ClipboardData.b("ݲၴྲྀ൸᥺ቼݾ", a_);
		}

		// Token: 0x06004633 RID: 17971 RVA: 0x0040DE5C File Offset: 0x0040CE5C
		// Note: this type is marked as 'beforefieldinit'.
		static TextBoxItemCollection()
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
			TextBoxItemCollection.ᜀ = new Type[]
			{
				typeof(TextBox)
			};
		}

		// Token: 0x04003690 RID: 13968
		private float \u2593\u009C\u009A\u009C;

		// Token: 0x04003691 RID: 13969
		private string \u2593\u00AE\u0081\u008E;

		// Token: 0x04003692 RID: 13970
		private long \u25D8\u00A6\u00A5\u008A;

		// Token: 0x04003693 RID: 13971
		private float[] \u25D8\u009E\u008B\u0097;

		// Token: 0x04003694 RID: 13972
		private long[] \u2460\u007F\u007F\u009B;

		// Token: 0x04003695 RID: 13973
		private string \u25D9\u00A0\u0098\u0091;

		// Token: 0x04003696 RID: 13974
		private new static readonly Type[] ᜀ;
	}
}
