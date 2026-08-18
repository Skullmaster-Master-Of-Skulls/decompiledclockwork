using System;
using System.Collections;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x02000527 RID: 1319
	public abstract class DocumentSubsetCollection : IDocumentObjectCollection
	{
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600451D RID: 17693 RVA: 0x00406364 File Offset: 0x00405364
		public Document Document
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
				return this.ᜀ.Document;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600451E RID: 17694 RVA: 0x004063AC File Offset: 0x004053AC
		public DocumentObject Owner
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
				return this.ᜀ.Owner;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600451F RID: 17695 RVA: 0x004063F4 File Offset: 0x004053F4
		public int Count
		{
			get
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
				return this.ᜄ;
			}
		}

		// Token: 0x1700052D RID: 1325
		public DocumentObject this[int index]
		{
			get
			{
				int a_ = 16;
				while (this.ᜀ.Count < 1)
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
						if (true)
						{
						}
						throw new ArgumentOutOfRangeException(ClipboardData.b("ήᙷṹ᥻ٽ", a_));
					}
				}
				return this.GetByIndex(index);
			}
		}

		// Token: 0x06004521 RID: 17697 RVA: 0x004064A8 File Offset: 0x004054A8
		internal DocumentSubsetCollection(DocumentObjectCollection A_0, DocumentObjectType A_1)
		{
			this.ᜀ = A_0;
			this.ᜁ = A_1;
			this.ᜀ();
			A_0.ᜀ.ᜁ(new DocumentObjectCollection.ChangeItems(this.ᜀ));
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x004064F4 File Offset: 0x004054F4
		public void Clear()
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
			this.ᜀ.ᜀ(this.ᜁ);
			this.ᜄ = 0;
			this.ᜂ = -1;
			this.ᜃ = -1;
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x00406558 File Offset: 0x00405558
		public IEnumerator GetEnumerator()
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
			return new DocumentSubsetCollection.SubSetEnumerator(this);
		}

		// Token: 0x06004524 RID: 17700 RVA: 0x0040659C File Offset: 0x0040559C
		internal DocumentObject ᜁ(DocumentObject A_0)
		{
			int num;
			for (;;)
			{
				num = this.ᜆ(A_0);
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_83;
						default:
							if (false)
							{
							}
							if (num > this.Count - 2)
							{
								num2 = 3;
								continue;
							}
							goto IL_83;
						}
						break;
					case 1:
						if (true)
						{
						}
						if (num >= 0)
						{
							num2 = 2;
							continue;
						}
						goto IL_3E;
					case 2:
						num2 = 0;
						continue;
					case 3:
						goto IL_81;
					}
					break;
				}
			}
			IL_3E:
			return null;
			IL_81:
			goto IL_3E;
			IL_83:
			return this.GetByIndex(num + 1);
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x00406638 File Offset: 0x00405638
		internal DocumentObject ᜃ(DocumentObject A_0)
		{
			int num;
			for (;;)
			{
				num = this.ᜆ(A_0);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num2 = 1;
						continue;
					case 1:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_83;
						default:
							if (false)
							{
							}
							if (num > this.Count - 1)
							{
								num2 = 3;
								continue;
							}
							goto IL_83;
						}
						break;
					case 2:
						if (num >= 1)
						{
							num2 = 0;
							continue;
						}
						goto IL_36;
					case 3:
						goto IL_81;
					}
					break;
				}
			}
			IL_36:
			return null;
			IL_81:
			goto IL_36;
			IL_83:
			return this.GetByIndex(num - 1);
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x004066D4 File Offset: 0x004056D4
		internal int ᜄ(DocumentObject A_0)
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
			this.ᜀ(A_0);
			this.ᜀ.Add(A_0);
			return this.ᜄ - 1;
		}

		// Token: 0x06004527 RID: 17703 RVA: 0x0040672C File Offset: 0x0040572C
		internal bool ᜅ(DocumentObject A_0)
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
			this.ᜀ(A_0);
			return this.ᜀ.Contains(A_0);
		}

		// Token: 0x06004528 RID: 17704 RVA: 0x0040677C File Offset: 0x0040577C
		internal int ᜆ(DocumentObject A_0)
		{
			int num;
			for (;;)
			{
				this.ᜀ(A_0);
				num = 0;
				int num2 = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						return num;
					case 1:
						return -1;
					case 2:
						goto IL_90;
					case 3:
						if (num >= this.Count)
						{
							num2 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_83;
						default:
							if (false)
							{
							}
							num2 = 4;
							continue;
						}
						break;
					case 4:
						if (this.GetByIndex(num) == A_0)
						{
							goto IL_83;
						}
						num++;
						num2 = 5;
						continue;
					case 5:
						goto IL_90;
					}
					break;
					IL_83:
					num2 = 0;
					continue;
					IL_90:
					num2 = 3;
				}
			}
			return num;
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x0040683C File Offset: 0x0040583C
		internal int ᜀ(int A_0, DocumentObject A_1)
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
			int num = this.ᜀ(A_0);
			this.ᜀ.Insert(A_0, A_1);
			return num + 1;
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x00406894 File Offset: 0x00405894
		internal void ᜂ(DocumentObject A_0)
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
			this.ᜀ(A_0);
			this.ᜀ.Remove(A_0);
		}

		// Token: 0x0600452B RID: 17707 RVA: 0x004068E4 File Offset: 0x004058E4
		internal void ᜁ(int A_0)
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
			int index = this.ᜀ(A_0);
			this.ᜀ.RemoveAt(index);
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x00406934 File Offset: 0x00405934
		protected DocumentObject GetByIndex(int index)
		{
			int num = 12;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (this.ᜃ < 0)
					{
						num = 11;
						continue;
					}
					goto IL_19E;
				case 1:
				{
					if (index == this.ᜂ)
					{
						num = 4;
						continue;
					}
					bool flag;
					this.ᜃ = this.ᜀ.ᜀ(this.ᜃ, this.ᜁ, flag);
					num = 10;
					continue;
				}
				case 2:
					goto IL_D7;
				case 3:
					goto IL_95;
				case 4:
					goto IL_D7;
				case 5:
				{
					if (index == this.ᜂ)
					{
						num = 9;
						continue;
					}
					bool flag = this.ᜂ < index;
					num = 3;
					continue;
				}
				case 6:
					goto IL_164;
				case 7:
					num = 5;
					continue;
				case 8:
					goto IL_95;
				case 9:
					goto IL_103;
				case 10:
					for (;;)
					{
						bool flag;
						this.ᜂ += (flag ? 1 : -1);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_188;
						}
					}
					IL_188:
					if (false)
					{
					}
					num = 8;
					continue;
				case 11:
					this.ᜃ = this.ᜀ(index);
					this.ᜂ = index;
					num = 6;
					continue;
				}
				if (this.ᜃ >= 0)
				{
					num = 7;
					continue;
				}
				goto IL_103;
				IL_95:
				num = 1;
				continue;
				IL_D7:
				num = 0;
				continue;
				IL_103:
				this.ᜃ = this.ᜀ(index);
				this.ᜂ = index;
				num = 2;
			}
			IL_164:
			IL_19E:
			return this.ᜀ[this.ᜃ];
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x00406AF0 File Offset: 0x00405AF0
		private int ᜀ(int A_0)
		{
			switch (0)
			{
			default:
				if (true)
				{
				}
				for (;;)
				{
					IL_4D:
					int num;
					int num2;
					int num3;
					int count;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_CA:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						num2 = 0;
						num3 = 0;
						count = this.ᜀ.Count;
						num = 0;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_82;
						case 1:
						{
							if (num3 >= count)
							{
								num = 5;
								continue;
							}
							DocumentObject documentObject = ((IDocumentObjectCollection)this.ᜀ)[num3];
							num = 3;
							continue;
						}
						case 2:
							num = 8;
							continue;
						case 3:
						{
							DocumentObject documentObject;
							if (documentObject.DocumentObjectType == this.ᜁ)
							{
								num = 2;
								continue;
							}
							goto IL_84;
						}
						case 4:
							goto IL_84;
						case 5:
							return -1;
						case 6:
							return num3;
						case 7:
							goto IL_91;
						case 8:
							if (num2 == A_0)
							{
								num = 6;
								continue;
							}
							num2++;
							num = 4;
							continue;
						}
						goto IL_4D;
						IL_84:
						num3++;
						num = 7;
					}
					IL_91:
					IL_82:
					goto IL_CA;
				}
				return -1;
			}
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x00406C1C File Offset: 0x00405C1C
		private void ᜀ()
		{
			for (;;)
			{
				int num = -1;
				this.ᜄ = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_69;
					case 1:
						if (true)
						{
						}
						if (num >= 0)
						{
							num2 = 2;
							continue;
						}
						return;
					case 2:
						this.ᜄ++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_67;
						default:
							if (false)
							{
							}
							num2 = 3;
							continue;
						}
						break;
					case 3:
						goto IL_67;
					}
					break;
					IL_69:
					num = this.ᜀ.ᜀ(num, this.ᜁ, true);
					num2 = 1;
					continue;
					IL_67:
					goto IL_69;
				}
			}
		}

		// Token: 0x0600452F RID: 17711 RVA: 0x00406CCC File Offset: 0x00405CCC
		private void ᜀ(DocumentObject A_0)
		{
			int a_ = 2;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.DocumentObjectType != this.ᜁ)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					goto IL_7A;
				case 2:
					goto IL_34;
				}
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					num = 0;
				}
			}
			IL_34:
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_3E:
				throw new ArgumentException(ClipboardData.b("Ⅷѩᩫ཭ᱯ᭱ၳ噵ᵷᑹࡻ᝽ﮁꒃ憎", a_));
			default:
				if (false)
				{
				}
				throw new ArgumentNullException(ClipboardData.b("൧ѩᡫݭѯୱ", a_));
			}
			IL_7A:
			goto IL_3E;
		}

		// Token: 0x06004530 RID: 17712 RVA: 0x00406D88 File Offset: 0x00405D88
		private void ᜀ(DocumentObjectCollection.ChangeItemsType A_0, DocumentObject A_1)
		{
			for (;;)
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_1.DocumentObjectType == this.ᜁ)
						{
							num = 3;
							continue;
						}
						goto IL_F2;
					case 1:
						if (A_1.DocumentObjectType == this.ᜁ)
						{
							num = 5;
							continue;
						}
						goto IL_F2;
					case 2:
						switch (A_0)
						{
						case DocumentObjectCollection.ChangeItemsType.Add:
							num = 0;
							continue;
						case DocumentObjectCollection.ChangeItemsType.Remove:
							num = 1;
							continue;
						case DocumentObjectCollection.ChangeItemsType.Clear:
							this.ᜄ = 0;
							num = 6;
							continue;
						default:
							num = 4;
							continue;
						}
						break;
					case 3:
						goto IL_C7;
					case 4:
						return;
					case 5:
						goto IL_ED;
					case 6:
						goto IL_A1;
					}
					break;
				}
			}
			return;
			IL_A1:
			goto IL_F2;
			IL_C7:
			this.ᜄ++;
			return;
			IL_ED:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_C7;
			default:
				if (false)
				{
				}
				this.ᜄ--;
				return;
			}
			IL_F2:
			if (true)
			{
			}
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x00406E90 File Offset: 0x00405E90
		internal void ᜁ()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜂ = -1;
			this.ᜃ = -1;
		}

		// Token: 0x0400364E RID: 13902
		private DocumentObjectCollection ᜀ;

		// Token: 0x0400364F RID: 13903
		private DocumentObjectType ᜁ;

		// Token: 0x04003650 RID: 13904
		private int ᜂ = -1;

		// Token: 0x04003651 RID: 13905
		private int ᜃ = -1;

		// Token: 0x04003652 RID: 13906
		private int ᜄ;

		// Token: 0x02000528 RID: 1320
		public class SubSetEnumerator : IEnumerator
		{
			// Token: 0x06004532 RID: 17714 RVA: 0x00406EDC File Offset: 0x00405EDC
			public SubSetEnumerator(DocumentSubsetCollection enColl)
			{
				this.ᜁ = enColl;
			}

			// Token: 0x1700052E RID: 1326
			// (get) Token: 0x06004533 RID: 17715 RVA: 0x00406F00 File Offset: 0x00405F00
			public object Current
			{
				get
				{
					while (this.ᜀ >= 0)
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
							return this.ᜁ.ᜀ[this.ᜀ];
						}
					}
					if (true)
					{
					}
					return null;
				}
			}

			// Token: 0x06004534 RID: 17716 RVA: 0x00406F60 File Offset: 0x00405F60
			public bool MoveNext()
			{
				int num;
				for (;;)
				{
					num = this.ᜁ.ᜀ.ᜀ(this.ᜀ, this.ᜁ.ᜁ, true);
					if (num < 0)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_53;
					}
				}
				if (true)
				{
				}
				return false;
				IL_53:
				if (false)
				{
				}
				this.ᜀ = num;
				return true;
			}

			// Token: 0x06004535 RID: 17717 RVA: 0x00406FD0 File Offset: 0x00405FD0
			public void Reset()
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
				this.ᜀ = -1;
			}

			// Token: 0x04003653 RID: 13907
			private byte \u2460\u0092\u008B\u009E;

			// Token: 0x04003654 RID: 13908
			private float \u2609\u0095\u0099\u0086;

			// Token: 0x04003655 RID: 13909
			private long \u25D9\u00A2\u007F\u00A8;

			// Token: 0x04003656 RID: 13910
			private string[] \u2609\u009E\u00A0\u0092;

			// Token: 0x04003657 RID: 13911
			private bool[] \u25D9\u00A0\u00A7\u0082;

			// Token: 0x04003658 RID: 13912
			private int ᜀ = -1;

			// Token: 0x04003659 RID: 13913
			private DocumentSubsetCollection ᜁ;
		}
	}
}
