using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x0200053D RID: 1341
	public class BodyRegionCollection : DocumentObjectCollection
	{
		// Token: 0x17000550 RID: 1360
		internal BodyRegion this[int A_0]
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
				return (BodyRegion)base[A_0];
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06004617 RID: 17943 RVA: 0x0040D58C File Offset: 0x0040C58C
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
				return BodyRegionCollection.ᜀ;
			}
		}

		// Token: 0x06004618 RID: 17944 RVA: 0x0040D5CC File Offset: 0x0040C5CC
		public BodyRegionCollection(Body body) : base(body.Document, body)
		{
		}

		// Token: 0x06004619 RID: 17945 RVA: 0x0040D5E8 File Offset: 0x0040C5E8
		internal BodyRegionCollection(Document A_0) : base(A_0, null)
		{
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x0040D600 File Offset: 0x0040C600
		protected override string GetTagItemName()
		{
			int a_ = 15;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return ClipboardData.b("ᱴͶᱸᙺ", a_);
		}

		// Token: 0x0600461B RID: 17947 RVA: 0x0040D654 File Offset: 0x0040C654
		protected override OwnerHolder CreateItem(IXDLSContentReader reader)
		{
			int a_ = 11;
			for (;;)
			{
				IL_21:
				string attributeValue = reader.GetAttributeValue(ClipboardData.b("հੲմቶ", a_));
				for (;;)
				{
					IL_36:
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							string a;
							if ((a = attributeValue) != null)
							{
								num = 1;
								continue;
							}
							goto IL_B2;
						}
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_36;
							default:
								if (false)
								{
								}
								num = 3;
								continue;
							}
							break;
						case 2:
							goto IL_B0;
						case 3:
						{
							string a;
							if (a == ClipboardData.b("╰ቲ᝴᭶ᱸ", a_))
							{
								if (true)
								{
								}
								num = 2;
								continue;
							}
							goto IL_B2;
						}
						}
						goto IL_21;
					}
				}
			}
			IL_B0:
			return new Table(base.Document);
			IL_B2:
			return new Paragraph(base.Document);
		}

		// Token: 0x0600461C RID: 17948 RVA: 0x0040D720 File Offset: 0x0040C720
		// Note: this type is marked as 'beforefieldinit'.
		static BodyRegionCollection()
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
			BodyRegionCollection.ᜀ = new Type[]
			{
				typeof(Table),
				typeof(Paragraph),
				typeof(spr\u1AE7)
			};
		}

		// Token: 0x04003686 RID: 13958
		private byte \u2609\u009A\u0080\u0098;

		// Token: 0x04003687 RID: 13959
		private float \u2593\u009B\u0082\u0080;

		// Token: 0x04003688 RID: 13960
		private int[] \u2609\u0089\u0093\u00A3;

		// Token: 0x04003689 RID: 13961
		private int \u2593\u008C\u009B\u00AE;

		// Token: 0x0400368A RID: 13962
		private new static readonly Type[] ᜀ;
	}
}
