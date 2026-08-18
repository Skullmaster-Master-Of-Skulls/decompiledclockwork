using System;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Documents.XML;
using Spire.Doc.Interface;

namespace Spire.Doc
{
	// Token: 0x0200008E RID: 142
	public abstract class DocumentObject : DocumentSerializable, IDocumentObject
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00009850 File Offset: 0x00008850
		internal DocumentObject ParentObject
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
				return this.Owner;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00009894 File Offset: 0x00008894
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
				return (DocumentObject)base.OwnerBase;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000081 RID: 129
		public abstract DocumentObjectType DocumentObjectType { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000098DC File Offset: 0x000088DC
		public IDocumentObject NextSibling
		{
			get
			{
				ICompositeObject compositeObject;
				for (;;)
				{
					compositeObject = (this.Owner as ICompositeObject);
					if (compositeObject == null)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_33;
					}
				}
				return null;
				IL_33:
				if (true)
				{
				}
				if (false)
				{
				}
				return compositeObject.ChildObjects.ᜁ(this);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00009938 File Offset: 0x00008938
		public IDocumentObject PreviousSibling
		{
			get
			{
				ICompositeObject compositeObject;
				for (;;)
				{
					compositeObject = (this.Owner as ICompositeObject);
					if (compositeObject == null)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_33;
					}
				}
				return null;
				IL_33:
				if (true)
				{
				}
				if (false)
				{
				}
				return compositeObject.ChildObjects.ᜂ(this);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00009994 File Offset: 0x00008994
		public bool IsComposite
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
				return this is ICompositeObject;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000099D8 File Offset: 0x000089D8
		internal bool DeepDetached
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						return false;
					case 2:
						if (this.Owner != null)
						{
							num = 3;
							continue;
						}
						return true;
					case 3:
						goto IL_5E;
					}
					if (this.DocumentObjectType == DocumentObjectType.Document)
					{
						if (true)
						{
						}
						num = 1;
					}
					else
					{
						num = 2;
					}
				}
				return false;
				IL_5E:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return true;
				default:
					if (false)
					{
					}
					return this.Owner.DeepDetached;
				}
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00009A70 File Offset: 0x00008A70
		protected DocumentObject(Document doc, DocumentObject owner) : base(doc, owner)
		{
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00009A88 File Offset: 0x00008A88
		public DocumentObject Clone()
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
			return (DocumentObject)this.CloneImpl();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00009AD0 File Offset: 0x00008AD0
		internal virtual void CloneCommit()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					ICompositeObject compositeObject = this as ICompositeObject;
					int num = 4;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							goto IL_9C;
						case 1:
						{
							num2--;
							int num3;
							num3--;
							num = 3;
							continue;
						}
						case 2:
						{
							DocumentObjectCollection childObjects = compositeObject.ChildObjects;
							num2 = 0;
							int num3 = childObjects.Count;
							num = 5;
							continue;
						}
						case 3:
							goto IL_5C;
						case 4:
							if (compositeObject != null)
							{
								num = 2;
								continue;
							}
							return;
						case 5:
							goto IL_9A;
						case 6:
						{
							int num3;
							DocumentObjectCollection childObjects;
							if (childObjects.Count < num3)
							{
								num = 1;
								continue;
							}
							goto IL_5C;
						}
						case 7:
							return;
						case 8:
						{
							if (true)
							{
							}
							int num3;
							if (num2 < num3)
							{
								DocumentObjectCollection childObjects;
								DocumentObject documentObject = childObjects[num2];
								documentObject.CloneCommit();
								num = 6;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_9A;
							default:
								if (false)
								{
								}
								num = 7;
								continue;
							}
							break;
						}
						}
						break;
						IL_5C:
						num2++;
						num = 0;
						continue;
						IL_9C:
						num = 8;
						continue;
						IL_9A:
						goto IL_9C;
					}
				}
				return;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00009C00 File Offset: 0x00008C00
		internal virtual void RemoveSelf()
		{
			for (;;)
			{
				ICompositeObject compositeObject = this.Owner as ICompositeObject;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						compositeObject.ChildObjects.Remove(this);
						num = 3;
						continue;
					case 1:
						if (compositeObject.ChildObjects == null)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_43;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 2:
						if (true)
						{
						}
						if (compositeObject != null)
						{
							num = 4;
							continue;
						}
						return;
					case 3:
						return;
					case 4:
						goto IL_43;
					}
					break;
					IL_43:
					num = 1;
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00009CAC File Offset: 0x00008CAC
		internal int ឯ()
		{
			ICompositeObject compositeObject;
			for (;;)
			{
				compositeObject = (this.Owner as ICompositeObject);
				if (compositeObject != null)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_3E;
				}
			}
			return compositeObject.ChildObjects.IndexOf(this);
			IL_3E:
			if (true)
			{
			}
			if (false)
			{
			}
			return -1;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00009D08 File Offset: 0x00008D08
		internal bool ᜄ(DocumentObject A_0)
		{
			bool result;
			for (;;)
			{
				result = false;
				OwnerHolder ownerHolder = A_0.OwnerBase;
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_9B;
					case 1:
						goto IL_9B;
					case 2:
						result = true;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_84;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 3:
						if (ownerHolder == this)
						{
							goto IL_84;
						}
						ownerHolder = ownerHolder.OwnerBase;
						num = 0;
						continue;
					case 4:
						return result;
					case 5:
						return result;
					case 6:
						if (ownerHolder == null)
						{
							num = 4;
							continue;
						}
						num = 3;
						continue;
					}
					break;
					IL_84:
					num = 2;
					continue;
					IL_9B:
					num = 6;
				}
			}
			return result;
		}
	}
}
