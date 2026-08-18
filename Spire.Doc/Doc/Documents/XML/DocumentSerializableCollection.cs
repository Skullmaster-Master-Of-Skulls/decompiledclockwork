using System;
using System.Collections;
using Spire.Doc.Collections;
using Spire.Doc.Interface;

namespace Spire.Doc.Documents.XML
{
	// Token: 0x020002D6 RID: 726
	public abstract class DocumentSerializableCollection : CollectionEx, IXDLSSerializableCollection
	{
		// Token: 0x06002785 RID: 10117 RVA: 0x0027B14C File Offset: 0x0027A14C
		protected DocumentSerializableCollection(Document doc, OwnerHolder owner) : base(doc, owner)
		{
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x0027B164 File Offset: 0x0027A164
		IDocumentSerializable IXDLSSerializableCollection.AddNewItem(IXDLSContentReader reader)
		{
			OwnerHolder ownerHolder;
			for (;;)
			{
				ownerHolder = this.CreateItem(reader);
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (ownerHolder != null)
						{
							num = 2;
							continue;
						}
						goto IL_82;
					case 1:
						goto IL_76;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							break;
						}
						base.InnerList.Add(ownerHolder);
						ownerHolder.ᜀ(base.OwnerBase);
						num = 1;
						continue;
					}
					break;
				}
			}
			IL_76:
			IL_82:
			return ownerHolder as IDocumentSerializable;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06002787 RID: 10119 RVA: 0x0027B1FC File Offset: 0x0027A1FC
		string IXDLSSerializableCollection.TagItemName
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
				return this.GetTagItemName();
			}
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x0027B240 File Offset: 0x0027A240
		internal virtual void CloneToImpl(CollectionEx coll)
		{
			IEnumerator enumerator = base.InnerList.GetEnumerator();
			try
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_7E;
					case 1:
					{
						if (!enumerator.MoveNext())
						{
							num = 2;
							continue;
						}
						DocumentSerializable documentSerializable = (DocumentSerializable)enumerator.Current;
						coll.InnerList.Add(documentSerializable.ឱ());
						num = 3;
						continue;
					}
					case 2:
						num = 0;
						continue;
					}
					IL_5C:
					num = 1;
					continue;
					goto IL_5C;
				}
				IL_7E:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								break;
							}
							disposable.Dispose();
							num = 1;
							continue;
						case 1:
							goto IL_DA;
						case 2:
							if (disposable != null)
							{
								num = 0;
								continue;
							}
							goto IL_DC;
						}
						break;
					}
				}
				IL_DA:
				IL_DC:;
			}
			if (true)
			{
			}
		}

		// Token: 0x06002789 RID: 10121
		protected abstract string GetTagItemName();

		// Token: 0x0600278A RID: 10122
		protected abstract OwnerHolder CreateItem(IXDLSContentReader reader);
	}
}
