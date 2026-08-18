using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x0200053F RID: 1343
	public class DocumentItemCollection : DocumentObjectCollection
	{
		// Token: 0x17000554 RID: 1364
		internal sprᩍ this[int A_0]
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
				return (sprᩍ)base[A_0];
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06004628 RID: 17960 RVA: 0x0040DB08 File Offset: 0x0040CB08
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
				return DocumentItemCollection.ᜀ;
			}
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x0040DB48 File Offset: 0x0040CB48
		internal DocumentItemCollection(Document A_0, sprᩍ A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x0040DB60 File Offset: 0x0040CB60
		protected override string GetTagItemName()
		{
			int a_ = 3;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return ClipboardData.b("ᩨͪ౬Ὦᑰ", a_);
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x0040DBB4 File Offset: 0x0040CBB4
		protected override OwnerHolder CreateItem(IXDLSContentReader reader)
		{
			int a_ = 15;
			for (;;)
			{
				string attributeValue = reader.GetAttributeValue(ClipboardData.b("Ŵ๶ॸṺ", a_));
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_B0;
					case 1:
					{
						string a;
						if ((a = attributeValue) == null)
						{
							goto IL_B2;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B2;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					case 2:
					{
						string a;
						if (a == ClipboardData.b("ٴὶᡸ୺᡼", a_))
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						goto IL_B2;
					}
					case 3:
						num = 2;
						continue;
					}
					break;
				}
			}
			IL_B0:
			return new spr\u1937(base.Document);
			IL_B2:
			return null;
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x0040DC74 File Offset: 0x0040CC74
		// Note: this type is marked as 'beforefieldinit'.
		static DocumentItemCollection()
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
			DocumentItemCollection.ᜀ = new Type[]
			{
				typeof(sprᩍ)
			};
		}

		// Token: 0x0400368E RID: 13966
		private byte \u2609\u00AD\u0084\u0094;

		// Token: 0x0400368F RID: 13967
		private new static readonly Type[] ᜀ;
	}
}
