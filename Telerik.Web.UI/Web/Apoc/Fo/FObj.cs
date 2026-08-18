using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x02001394 RID: 5012
	internal class FObj : FONode
	{
		// Token: 0x0600D0DC RID: 53468 RVA: 0x002E3D51 File Offset: 0x002E1F51
		public static FObj.Maker GetMaker()
		{
			return new FObj.Maker();
		}

		// Token: 0x0600D0DD RID: 53469 RVA: 0x002E3D58 File Offset: 0x002E1F58
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected FObj(FObj parent, PropertyList propertyList) : base(parent)
		{
			this.properties = propertyList;
			propertyList.FObj = this;
			this.propMgr = this.MakePropertyManager(propertyList);
			this.name = "default FO";
			this.SetWritingMode();
		}

		// Token: 0x0600D0DE RID: 53470 RVA: 0x002E3D8D File Offset: 0x002E1F8D
		protected PropertyManager MakePropertyManager(PropertyList propertyList)
		{
			return new PropertyManager(propertyList);
		}

		// Token: 0x0600D0DF RID: 53471 RVA: 0x002E3D95 File Offset: 0x002E1F95
		protected internal virtual void AddCharacters(char[] data, int start, int length)
		{
		}

		// Token: 0x0600D0E0 RID: 53472 RVA: 0x002E3D97 File Offset: 0x002E1F97
		public override Status Layout(Area area)
		{
			return new Status(1);
		}

		// Token: 0x0600D0E1 RID: 53473 RVA: 0x002E3D9F File Offset: 0x002E1F9F
		public string GetName()
		{
			return this.name;
		}

		// Token: 0x0600D0E2 RID: 53474 RVA: 0x002E3DA7 File Offset: 0x002E1FA7
		protected internal virtual void Start()
		{
		}

		// Token: 0x0600D0E3 RID: 53475 RVA: 0x002E3DA9 File Offset: 0x002E1FA9
		protected internal virtual void End()
		{
		}

		// Token: 0x0600D0E4 RID: 53476 RVA: 0x002E3DAB File Offset: 0x002E1FAB
		public override Property GetProperty(string name)
		{
			return this.properties.GetProperty(name);
		}

		// Token: 0x0600D0E5 RID: 53477 RVA: 0x002E3DB9 File Offset: 0x002E1FB9
		public virtual int GetContentWidth()
		{
			return 0;
		}

		// Token: 0x0600D0E6 RID: 53478 RVA: 0x002E3DBC File Offset: 0x002E1FBC
		public virtual void RemoveID(IDReferences idReferences)
		{
			if (this.properties.GetProperty("id") == null || this.properties.GetProperty("id").GetString() == null)
			{
				return;
			}
			idReferences.RemoveID(this.properties.GetProperty("id").GetString());
			int count = this.children.Count;
			for (int i = 0; i < count; i++)
			{
				FONode fonode = (FONode)this.children[i];
				FObj fobj = fonode as FObj;
				if (fobj != null)
				{
					fobj.RemoveID(idReferences);
				}
			}
		}

		// Token: 0x0600D0E7 RID: 53479 RVA: 0x002E3E49 File Offset: 0x002E2049
		public virtual bool GeneratesReferenceAreas()
		{
			return false;
		}

		// Token: 0x0600D0E8 RID: 53480 RVA: 0x002E3E4C File Offset: 0x002E204C
		protected virtual void SetWritingMode()
		{
			FObj fobj = this;
			FObj parent;
			while (!fobj.GeneratesReferenceAreas() && (parent = fobj.getParent()) != null)
			{
				fobj = parent;
			}
			this.properties.SetWritingMode(fobj.GetProperty("writing-mode").GetEnum());
		}

		// Token: 0x0600D0E9 RID: 53481 RVA: 0x002E3E8C File Offset: 0x002E208C
		public void AddMarker(string markerClassName)
		{
			if (this.children != null)
			{
				for (int i = 0; i < this.children.Count; i++)
				{
					FONode fonode = (FONode)this.children[i];
					if (!fonode.MayPrecedeMarker())
					{
						throw new ApocException(string.Format("A fo:marker must be an initial child of '{0}'", this.GetName()));
					}
				}
			}
			if (this.markerClassNames == null)
			{
				this.markerClassNames = new Hashtable();
				this.markerClassNames.Add(markerClassName, string.Empty);
				return;
			}
			if (!this.markerClassNames.ContainsKey(markerClassName))
			{
				this.markerClassNames.Add(markerClassName, string.Empty);
				return;
			}
			throw new ApocException(string.Format("marker-class-name '{0}' already exists for this parent", markerClassName));
		}

		// Token: 0x04003810 RID: 14352
		public PropertyList properties;

		// Token: 0x04003811 RID: 14353
		protected PropertyManager propMgr;

		// Token: 0x04003812 RID: 14354
		protected string name;

		// Token: 0x04003813 RID: 14355
		private Hashtable markerClassNames;

		// Token: 0x02001395 RID: 5013
		internal class Maker
		{
			// Token: 0x0600D0EA RID: 53482 RVA: 0x002E3F3C File Offset: 0x002E213C
			public virtual FObj Make(FObj parent, PropertyList propertyList)
			{
				return new FObj(parent, propertyList);
			}
		}
	}
}
